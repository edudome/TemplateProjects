import React, { Component } from 'react';
import { Header } from './Header';
import { Formulario } from './Formulario';
import { ListaRecetas } from './ListaRecetas';

import { CategoriasProvider } from './context/CategoriasContext';
import { RecetasProvider } from './context/RecetasContext';
import { ModalProvider } from './context/ModalContext';

export function Bebidas() {
    return (
        <CategoriasProvider>
            <RecetasProvider>
                <ModalProvider>
                    <Header />
                    <div className="container mt-5">
                        <div className="row">
                            {<Formulario />}
                        </div>
                        <ListaRecetas />
                    </div>
                </ModalProvider>
            </RecetasProvider>
        </CategoriasProvider>
    );
}
