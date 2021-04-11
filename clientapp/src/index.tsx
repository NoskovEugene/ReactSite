import React from 'react';
import {render as reactRender} from "react-dom";
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import {ApplicationStartup} from "./Core/ApplicationStartup";

ApplicationStartup().then(x => {
    const root = document.getElementById("root");
    reactRender(
        <React.StrictMode>
            {x.app}
        </React.StrictMode>, root);
})
    .catch(e => {
        console.log(e);
        alert("cant initialize")
    });

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
