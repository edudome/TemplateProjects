import React, { useContext, useState } from 'react';
import { ModalContext } from './context/ModalContext';


export function Receta({ receta }) {

    const [open, setOpen] = useState(false);

    const handleOpen = () => {
        setOpen(open ? false : true);
    }

    const instruccionesStyle = {
        display: (open) ? "block" : "none"
    };

    // extraer los valores del context
    const { informacion, guardarIdReceta, guardarReceta } = useContext(ModalContext);

    // Muestra y formatea los ingredientes
    const mostrarIngredientes = informacion => {
        let ingredientes = [];
        for (let i = 1; i < 16; i++) {
            if (informacion[`strIngredient${i}`]) {
                ingredientes.push(
                    <li> {informacion[`strIngredient${i}`]}  {informacion[`strMeasure${i}`]}</li>
                )
            }
        }

        return ingredientes;
    }

    return (
        <div className="col-md-4 mb-3">
            <div className="card">
                <h2 className="card-header">{receta.strDrink}</h2>

                <img className="card-img-top" src={receta.strDrinkThumb} alt={`Imagen de ${receta.strDrink}`} />

                <div className="card-body">
                    <button
                        type="button"
                        className="btn btn-block btn-primary"
                        onClick={() => {
                            guardarIdReceta(receta.idDrink);
                            handleOpen();
                        }}
                    >
                        Ver Receta
                    </button>


                    <div style={instruccionesStyle}>
                        <h2>{informacion.strDrink}</h2>
                        <h3 className="mt-4">Instrucciones</h3>
                        <p>
                            {informacion.strInstructions}
                        </p>

                        <img className="img-fluid my-4" src={informacion.strDrinkThumb} />

                        <h3>Ingredientes y cantidades</h3>
                        <ul>
                            {mostrarIngredientes(informacion)}
                        </ul>
                    </div>

                </div>
            </div>
        </div>
    );
}
