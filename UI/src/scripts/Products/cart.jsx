import * as React from "react";

import { CartProduct } from "./cartproduct";
export class Cart extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showProducts: false
        }
        this.toggleShow = this.toggleShow.bind(this);
        this.sendOrder = this.sendOrder.bind(this);
    }
    toggleShow() {
        this.setState({
            showProducts: !this.state.showProducts
        })
    }
    sendOrder(){
        this.props.onSendOrder();
    }
    render() {

        return (
            <div className="cart">
                <h5>Din kundvagn</h5>
                <p className="mybutton" onClick={this.toggleShow}>{this.state.showProducts ? "Dölj varor" : "Visa varor"}</p>
                {this.state.showProducts && <div>
                    <ul>{this.props.cart}</ul>
                    <ul className="topborder">
                        <li className="row noPaddingAndMargin">
                            <span className="col-7 noPaddingAndMargin">Summa:</span>
                            <div className="col-5 row noPaddingAndMargin">
                                <span className="col-6 inCartProduct"></span>
                                <span className="col-6 inCartProduct">{this.props.sum} kr</span>
                            </div>
                        </li>
                    </ul>
                    <button className="col-2" onClick={this.sendOrder}>Skicka order</button>
                </div>}
            </div>
        );
    }
}