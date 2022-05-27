import axios from 'axios';

// Axios Get Call
export const AxiosGetCall = async (url) => {
    try {
        const response = await axios.get(url);
        return response.data;
    }
    catch (error) {
        console.log('Axios request failed: ${error.response}');
        return error.response;
    }
}

// Axios Post Call
export const AxiosPostCall = async (url, data) => {
    try {
        const response = await axios.post(url, data);
        console.log('Returned data:', response.data);
        return response.data;
    }
    catch (error) {
        console.log('Axios request failed: ${error.response}');
    }
}