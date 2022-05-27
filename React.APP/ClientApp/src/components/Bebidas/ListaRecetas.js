import React, { useContext } from 'react';
import { Receta } from './Receta';
import { RecetasContext } from './context/RecetasContext';

export function ListaRecetas() {

    // extraer las recetas
    const { recetas } = useContext(RecetasContext);

    return (
        <div className="row mt-5">

            {recetas ? recetas.map(receta => (
                <Receta
                    key={receta.idDrink}
                    receta={receta}
                />
            )) : <p>No se encontraron recetas</p>}
        </div>
    );
}
