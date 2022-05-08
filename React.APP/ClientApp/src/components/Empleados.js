import React, { Component, useState } from 'react';

export function EmpleadosList({empleados}) {

    return (
        <ul>
            {empleados.map((empleado) => (<li></li>))}
        </ul>
    )
}

export function ListarEmpleados() {

    const [empleados, setEmpleados] = useState([
        { id: 1, Nombre: "Eduardo", Habilitado: true }
    ]);

    return (
        <div>
            <div>Lista de Empleados:</div>
            <EmpleadosList empleados={empleados} />
        </div>
    );
}
