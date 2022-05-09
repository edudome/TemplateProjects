import React, { Fragment } from 'react';

export default function Header({ titulo }) {
    return (
        <Fragment>
            <h4 className="encabezado">{titulo}</h4>
        </Fragment>
    )
}
