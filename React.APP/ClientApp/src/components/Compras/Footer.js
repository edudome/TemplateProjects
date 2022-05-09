import React, { Fragment } from 'react';

export default function Footer({ fecha }) {
    return (
        <Fragment>
            <footer>
                <p>Todos los derechos reservados &copy; {fecha} </p>
            </footer>
        </Fragment>
    );
}
