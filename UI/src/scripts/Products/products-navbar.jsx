import * as React from "react";
import * as ReactDOM from "react-dom";

import { Product } from "./product";


export class ProductNavbar extends React.Component {
    constructor(props) {
        super(props);

        this.toggleComponent = this.toggleComponent.bind(this);

    }
    componentDidMount() { this.toggleComponent(1) }
    
    toggleComponent(component) {

        let fetchurl = this.props.myApiUrl + "/products";        
        fetch(fetchurl, {
            'method' : 'get',
            'headers' : {
                'accept': '*/*',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('jwtbearer')}`
            } 
        })
        .then(res => {
            if (res.status == 200){
                res.json().then(list => {
                    this.sendList(list);
                })
            }
            else {
                alert("Något gick fel, kontrollera om du är inloggad.")
            }
        })
        this.props.showPage(component);
    }
    sendList = (list) => {        
        this.props.onGetList(list);
    }
    render() {
        return (
            <nav className="navbar navbar-default">
                <div className="container-fluid">
                    <button className="navbar-brand btn" onClick={() => this.toggleComponent(1)}>Se produkter</button>
                    <button disabled={!this.props.isAdmin} className="navbar-brand btn" onClick={() => this.toggleComponent(2)}>Admin</button>

                </div>
            </nav>
        );
    }
}



