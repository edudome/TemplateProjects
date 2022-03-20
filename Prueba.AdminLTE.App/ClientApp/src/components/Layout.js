import React, { Component } from 'react';
import { Header } from './Header';
import { Menu } from './Menu';
import { Dashboard } from './Dashboard';
import { Footer } from './Footer';

import './Layout.css'

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        {/*<Header/>*/}
        {/*<Menu/>*/}
        {/*<Dashboard/>*/}
        {/*{<Footer/>}*/}
      </div>
    );
  }
}
