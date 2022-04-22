import * as React from "react";

export class CartProduct extends React.Component {
    constructor(props) {
        super(props);

    }

    render() {
        return (
            <li className="row noPaddingAndMargin">
                <span className="col-7 noPaddingAndMargin">{this.props.name}</span>
                <div className="col-5 row noPaddingAndMargin">
                    <span className="col-6 inCartProduct">x {this.props.quantity}</span>
                    <span className="col-6 inCartProduct">{this.props.price} kr/st</span>
                </div>
            </li>
        );
    }
}