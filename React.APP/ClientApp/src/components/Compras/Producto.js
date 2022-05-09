import React, { Fragment } from 'react';
import { v4 as uuidv4 } from 'uuid';

export default function Producto({ producto, carrito, agregarProducto, productos }) {

    const { nombre, precio, id } = producto;

    // Agregar producto al carrito
    const seleccionarProducto = id => {
        const producto = productos.filter(producto => producto.id === id)[0];
        agregarProducto([
            ...carrito,
            { id: uuidv4(), nombre: producto.nombre, precio: producto.precio }
        ]);
    }

    // Elimina un producto del carrito
    const eliminarProducto = id => {
        const productos = carrito.filter(producto => producto.id !== id);

        // Colocar los productos en el state
        agregarProducto(productos)
    }

    return (
        <Fragment>
            <h5>{nombre}</h5>
            <p>${precio}</p>

            {productos
                ?
                (
                    <button
                        type="button"
                        onClick={() => seleccionarProducto(id)}
                    >Comprar</button>
                )
                :
                (
                    <button
                        type="button"
                        onClick={() => eliminarProducto(id)}
                    >Eliminar</button>
                )
            }
        </Fragment>
    );
}
