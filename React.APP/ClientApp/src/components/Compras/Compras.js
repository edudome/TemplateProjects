import React, { Fragment, useState } from 'react';
import { v4 as uuidv4 } from 'uuid';
import Header from './Header';
import Footer from './Footer';
import Producto from './Producto';
import Carrito from './Carrito';

export function Compras() {

    // Crear listado de productos
    const [productos, guardarProductos] = useState([
        { id: uuidv4(), nombre: 'Camisa ReactJS', precio: 50 },
        { id: uuidv4(), nombre: 'Camisa VueJS', precio: 40 },
        { id: uuidv4(), nombre: 'Camisa Node.js', precio: 30 },
        { id: uuidv4(), nombre: 'Camisa Angular', precio: 20 },
    ]);

    // State para un carrito de compras
    const [carrito, agregarProducto] = useState([]);


    // Obtener la fecha
    const fecha = new Date().getFullYear();

    return (
        <Fragment>
            <Header
                titulo='Tienda Virtual'
            />

            <h4>Lista de Productos</h4>
            {productos.map(producto => (
                <Producto
                    key={producto.id}
                    producto={producto}
                    productos={productos}
                    carrito={carrito}
                    agregarProducto={agregarProducto}
                />
            ))}

            <Carrito
                carrito={carrito}
                agregarProducto={agregarProducto}
            />

            <Footer
                fecha={fecha}
            />
        </Fragment>
    );
}
