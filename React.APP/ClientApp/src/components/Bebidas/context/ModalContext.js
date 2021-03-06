import React, { createContext, useEffect, useState } from 'react';
import { AxiosGetCall } from '../../Axios';

// crear el context
export const ModalContext = createContext();

export function ModalProvider(props) {

    // state del provider
    const [idreceta, guardarIdReceta] = useState(null);
    const [informacion, guardarReceta] = useState({});

    // una vez que tenemos una receta, llamar la api
    useEffect(() => {
        const obtenerReceta = async () => {
            if (!idreceta) return;

            const url = `https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i=${idreceta}`;

            const resultado = await AxiosGetCall(url);

            guardarReceta(resultado.drinks[0]);
        }
        obtenerReceta();
    }, [idreceta]);

    return (
        <ModalContext.Provider
            value={{
                informacion,
                guardarIdReceta,
                guardarReceta
            }}
        >
            {props.children}
        </ModalContext.Provider>
    );
}
