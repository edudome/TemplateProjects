import React, { createContext, useState, useEffect } from 'react';
import { AxiosGetCall } from '../../Axios';


export const RecetasContext = createContext();

export function RecetasProvider(props) {

    const [recetas, guardarRecetas] = useState([]);
    const [busqueda, buscarRecetas] = useState({
        nombre: '',
        categoria: ''
    });
    const [consultar, guardarConsultar] = useState(false);

    const { nombre, categoria } = busqueda;

    useEffect(() => {
        if (consultar) {
            const obtenerRecetas = async () => {

                const url = `https://www.thecocktaildb.com/api/json/v1/1/filter.php?i=${nombre}&c=${categoria}`;

                const resultado = await AxiosGetCall(url);

                if (resultado != null) {
                    guardarRecetas(resultado.drinks);
                }
                else {
                    console.log('No hay nada para guardar.');
                }
            }

            obtenerRecetas();
        }

    }, [busqueda]);

    return (
        <RecetasContext.Provider
            value={{
                recetas,
                buscarRecetas,
                guardarConsultar
            }}
        >
            {props.children}
        </RecetasContext.Provider>
    );
}
