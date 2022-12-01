import React from 'react';
import ReactDOM from 'react-dom';

const App = () => {
    return(
        <div>
            <h1>tekst2</h1>
            <h3>from inside src/app.js</h3>
        </div>
    )
}
ReactDOM.render(<App />, document.getElementById("root"))