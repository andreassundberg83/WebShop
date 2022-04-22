import * as React from "react";
export class Order extends React.Component {
    constructor(props) {
        super(props);

    }
    render() {
        let cart = [];
        let i = 1;
        this.props.cart.forEach(p => {  
            cart.push(            
                 <li key={i} className="row">
                <h6 className="col-12">{p.productName}</h6>
                <p className="col-12">Pris: {p.price}</p>                
                <p className="col-12">Antal: {p.quantity}</p>
            </li>)
            i++;
        })
        return (
            <li className="products">
                <h4>Ordernummer: {this.props.id}</h4>
                <h5>Status: {this.props.status}</h5>
                <p><b>Skapad:</b>{this.props.created}</p>
                <p><b>Senast uppdaterad:</b>{this.props.updated}</p>
                <h5>Varor:</h5>
                <ul>{cart}</ul>
                <p>Summa: {this.props.sum} kr</p>
            </li>
        );
    }
}