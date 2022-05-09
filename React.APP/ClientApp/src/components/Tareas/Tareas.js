import React, { useState, useRef, useEffect, Fragment } from 'react';
import { v4 as uuidv4 } from 'uuid';
import { TareaList } from './TareaList';

const KEY = "tareaApp.tareas"; // para identificar en el LocalStorage

export function Tareas() {

    const tareaNombreRef = useRef();
    const [tareas, setTareas] = useState([
        { id: 1, descripcion: "Tarea", completed: false },
    ]);

    useEffect(() => {
        const storedtareas = JSON.parse(localStorage.getItem(KEY));
        if (storedtareas) {
            setTareas(storedtareas);
        }
    }, []);

    useEffect(() => {
        localStorage.setItem(KEY, JSON.stringify(tareas));
    }, [tareas]);

    const toggleTarea = (id) => {
        const newTareas = [...tareas];
        const tarea = newTareas.find((tarea) => tarea.id === id);
        tarea.completed = !tarea.completed;
        setTareas(newTareas);
    };

    const handleTareaAdd = (event) => {
        const Nombre = tareaNombreRef.current.value;
        if (Nombre === "") return;

        setTareas((prevTareas) => {
            return [...prevTareas, { id: uuidv4(), descripcion: Nombre, completed: false }];
        });

        tareaNombreRef.current.value = null;
    };

    const handleClearAll = () => {
        const newTareas = tareas.filter((tarea) => !tarea.completed);
        setTareas(newTareas);
    };

    return (
        <Fragment>
            <Encabezado />
            <TareaList tareas={tareas} toggleTarea={toggleTarea} />
            <input ref={tareaNombreRef} type="text" placeholder="Nueva tarea" />
            <button onClick={handleTareaAdd}>Agregar</button>
            <button onClick={handleClearAll}>Eliminar</button>
            <div>
                Te quedan {tareas.filter((tarea) => !tarea.completed).length} tareas por
                terminar
            </div>
        </Fragment>
    );
}

export function Encabezado() {

    return (
        <Fragment>
            <h1>TAREAS</h1>
        </Fragment>
    );
}