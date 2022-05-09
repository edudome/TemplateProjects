﻿import React, { Fragment } from 'react';
import './carrito.css';
import Producto from './Producto';

export default function Carrito({ carrito, agregarProducto }) {
    return (
        <Fragment>
            <div className="carrito">
                <h2>Tu carrito de compras</h2>

                {carrito.length === 0
                    ? <p>No hay elementos en el carrito</p>
                    : carrito.map(producto => (
                        <Producto
                            key={producto.id}
                            producto={producto}
                            carrito={carrito}
                            agregarProducto={agregarProducto}
                        />
                    ))}
            </div>
        </Fragment>
    );
}