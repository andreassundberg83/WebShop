import * as React from "react";
export class Product extends React.Component {
    constructor(props) {
        super(props);

        this.getByCategory = this.getByCategory.bind(this);
        this.addToCart = this.addToCart.bind(this);

    }
    addToCart = () => this.props.productAdded(this.props.id);
    getByCategory(categoryId) {
        this.props.sortByCategory(categoryId);
    }
    render() {
        return (
            <li className="products col-md-12 col-lg-5 row">
                <h6 className="col-12">{this.props.name}</h6>
                <p className="col-12">{this.props.description}</p>
                <p className="col-12">Pris: {this.props.price}</p>
                <div className="col-12 row justify-content-between">
                    <span className="col-12 col-md-8 mybutton" onClick={() => this.getByCategory(this.props.categoryId)}>{this.props.categoryName}</span>
                    <button className="col-12 col-md-4 btn btn-success" onClick={this.addToCart}>Lägg i varukorg</button>
                </div>
            </li>
        );
    }
}