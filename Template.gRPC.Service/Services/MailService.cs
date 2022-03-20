﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Template.gRPC.Service.Models;
using Template.gRPC.Service.Repository;
using static Template.gRPC.Service.MailService;

namespace Template.gRPC.Service.Services
{
    public class MailService : MailServiceBase
    {
        private readonly ILogger _logger;
        private readonly MailQueueRepository _messageQueueRepository;

        public MailService(ILoggerFactory loggerFactory, MailQueueRepository messageQueueRepository)
        {
            _logger = loggerFactory.CreateLogger<MailService>();
            _messageQueueRepository = messageQueueRepository;
        }

        public async override Task Mailbox(
            IAsyncStreamReader<ForwardMailMessage> requestStream,
            IServerStreamWriter<MailboxMessage> responseStream,
            ServerCallContext context)
        {
            var mailboxName = context.RequestHeaders.Single(e => e.Key == "mailbox-name").Value;

            var mailQueue = _messageQueueRepository.GetMailQueue(mailboxName);

            _logger.LogInformation($"Connected to {mailboxName}");

            mailQueue.Changed += ReportChanges;

            try
            {
                while (await requestStream.MoveNext())
                {
                    if (mailQueue.TryForwardMail(out var message))
                    {
                        _logger.LogInformation($"Forwarded mail: {message.Content}");
                    }
                    else
                    {
                        _logger.LogWarning("No mail to forward.");
                    }
                }
            }
            finally
            {
                mailQueue.Changed -= ReportChanges;
            }

            _logger.LogInformation($"{mailboxName} disconnected");

            async Task ReportChanges(MailQueueChangeState state)
            {
                await responseStream.WriteAsync(new MailboxMessage
                {
                    Forwarded = state.ForwardedCount,
                    New = state.TotalCount - state.ForwardedCount,
                    Reason = state.Reason
                });
            }
        }
    }
}
