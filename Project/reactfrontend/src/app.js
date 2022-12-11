import React from 'react';
import ReactDOM from 'react-dom';
import {observable, configure, action} from 'mobx'
import { observer } from 'mobx-react'
import { BrowserRouter, Route } from 'react-router-dom';

const App = () => {
    return(
        <div>
            <h1>tekst2</h1>
            <h3>from inside src/app.js</h3>
        </div>
    )
}

ReactDOM.render((  
    <BrowserRouter>
        <App />
    </BrowserRouter>
    ),document.getElementById("root"))