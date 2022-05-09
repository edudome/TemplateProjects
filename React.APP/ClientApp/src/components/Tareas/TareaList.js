import React from 'react';
import { TareaItem } from './TareaItem';

export function TareaList({ tareas, toggleTarea }) {
    return (
        <ul>
            {tareas.map((tarea) => (
                <TareaItem key={tarea.id} tarea={tarea} toggleTarea={toggleTarea} />
            ))}
        </ul>
    );
}
