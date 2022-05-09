import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Tareas } from './components/Tareas/Tareas';
import { FetchData } from './components/Basics/FetchData';
import { Counter } from './components/Basics/Counter';
import { Greeting } from './components/Basics/Greeting';
import { GetStringAPI } from './components/Basics/GetStringAPI';
import { Compras } from './components/Compras/Compras';
import { Bebidas } from './components/Bebidas/Bebidas';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Tareas} />
                <Route path='/counter' component={Counter} />
                <Route path='/fetch-data' component={FetchData} />
                <Route path='/greeting' component={Greeting} />
                <Route path='/get-string-api' component={GetStringAPI} />
                <Route path='/compras' component={Compras} />
                <Route path='/bebidas' component={Bebidas} />
            </Layout>
        );
    }
}