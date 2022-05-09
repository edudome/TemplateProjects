import React, { useState, useEffect, Fragment } from 'react';

export function GetStringAPI() {

    // state de frases
    const [frase, guardarFrase] = useState({});

    const consultarAPI = async () => {
        const api = await fetch('https://breaking-bad-quotes.herokuapp.com/v1/quotes');
        const frase = await api.json()
        guardarFrase(frase[0]);
    }

    // Cargar una frase
    useEffect(() => {
        consultarAPI()
    }, []);

    return (
        <Fragment>
            <Frase frase={frase}/>
            <button onClick={consultarAPI}>Obtener Frase</button>
        </Fragment>
    );
}

export function Frase({ frase }) {
    return (
        <Fragment>
            <h1>{frase.quote} </h1>
            <p>- {frase.author} </p>
        </Fragment>
    );
}