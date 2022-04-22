import * as React from "react";
import { ProductsForm } from "./products-form";
import { ProductNavbar } from "./products-navbar";
import { Product } from "./product";
import { Cart } from "./cart"
import { CartProduct } from "./cartproduct";
export class Products extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showProducts: false,
            showAdmin: false,
            products: [],
            productsInCart: [],
            orderSum: 0

        }
        this.showSelectedPage = this.showSelectedPage.bind(this);
        this.addList = this.addList.bind(this);
        this.addToCart = this.addToCart.bind(this);
        this.sendOrder = this.sendOrder.bind(this);

    }
   
    showSelectedPage = (component) => {
        switch (component) {
            case 1: this.setState({
                showProducts: true,
                showAdmin: false
            })
                break;
            case 2: this.setState({
                showProducts: false,
                showAdmin: true
            })
                break;
            default: break;
        }
    }
    addList(list) {
        let productsList = [];
        list.forEach(x => productsList.push(<Product productAdded={this.addToCart} sortByCategory={this.categorySort} key={x.Id + x.name} {...x} />));
        this.setState({
            products: productsList
        })
    }
    categorySort = (id) => {
        let productsList = this.state.products.filter((x) => x.props.categoryId == id);
        this.setState({
            products: productsList
        })
    }
    addToCart = (id) => {
        let productsInCartList = this.state.productsInCart;
        let sum = this.state.orderSum;
        if (productsInCartList.some(x => x.props.id === id)) {
            const index = productsInCartList.findIndex(x => x.props.id === id)
            const product = productsInCartList[index];
            const cartProduct = <CartProduct key={id} price={product.props.price} id={product.props.id} quantity={product.props.quantity + 1} name={product.props.name} />;
            productsInCartList[index] = cartProduct;
            sum += product.props.price;
        }
        else {
            let product = this.state.products.find(x => x.props.id === id);
            const cartProduct = <CartProduct key={id} price={product.props.price} id={product.props.id} quantity={1} name={product.props.name} />;
            productsInCartList.push(cartProduct);
            sum += product.props.price;

        }
        this.setState({ productsInCart: productsInCartList });
        this.setState({ orderSum: sum });
    }
    sendOrder() {
        const fetchurl = this.props.myApiUrl + "/Orders"
        let cart = [];

        this.state.productsInCart.forEach(x => {
            cart.push({
                productId: x.props.id,
                quantity: x.props.quantity
            });
        });

        let data = JSON.stringify({
            userId: localStorage.getItem("userId"),
            cart: cart
        });
        
        fetch(fetchurl, {
            method: 'post',
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('jwtbearer')}`
            },
            body: data
        }).then(res => {
            console.log(res);
            if (res.status === 200) {
                res.json().then(x => {
                    let message = `Ordern lyckades!\n\nOrdernummer: ${x.id}\n`;
                    for (let i = 0; i < x.cart.length; i++) {
                        message += `\n${x.cart[i].quantity} st ${x.cart[i].productName}`;
                    }
                    message += `\nSumma: ${x.sum}`;
                    alert(message);
                    this.setState({
                        productsInCart: []
                    })
                });
            }
            else alert("Något gick fel.");

        });
    }
    render() {
        return (
            <div className="row">
                <ProductNavbar myApiUrl={this.props.myApiUrl} isAdmin={this.props.isAdmin} showPage={this.showSelectedPage} onGetList={this.addList} />
                {this.state.showAdmin && <ProductsForm myApiUrl={this.props.myApiUrl} products={this.state.products}/>}
                {this.state.showProducts && <div>
                    <Cart onSendOrder={this.sendOrder} sum={this.state.orderSum} cart={this.state.productsInCart} />
                    <ul className="row justify-content-around">{this.state.products}</ul>
                </div>}
            </div>
        );
    }
}