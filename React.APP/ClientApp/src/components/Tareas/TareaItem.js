import React from 'react';

export function TareaItem({ tarea, toggleTarea }) {
    const { id, descripcion, completed } = tarea;

    const handleTareaClick = () => {
        toggleTarea(id);
    };

    return (
        <li>
            <input type="checkbox" checked={completed} onChange={handleTareaClick} />
            {descripcion}
        </li>
    );
}
