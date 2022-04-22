import * as React from "react";

/*Det här är verkligen inte en snygg klass, men den funkar, så den får se ut så här*/

export class ProductsForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            description: '',
            categoryName: '',
            articleNumber: '',
            price: 0,
            selectedUpdateId: 0,
            updateName: '',
            updateDescription: '',
            updateCategoryName: '',
            updateArticleNumber: '',
            updatePrice: 0,
            selectedDeleteId: 0
        }
        this.changeName = this.changeName.bind(this);
        this.changeDescription = this.changeDescription.bind(this);
        this.changeCategory = this.changeCategory.bind(this);
        this.changeArticleNumber = this.changeArticleNumber.bind(this);
        this.changePrice = this.changePrice.bind(this);
        this.changeUpdateName = this.changeUpdateName.bind(this);
        this.changeUpdateDescription = this.changeUpdateDescription.bind(this);
        this.changeUpdateCategoryName = this.changeUpdateCategoryName.bind(this);
        this.changeUpdateArticleNumber = this.changeUpdateArticleNumber.bind(this);
        this.changeUpdatePrice = this.changeUpdatePrice.bind(this);
        this.changeSelectedUpdateId = this.changeSelectedUpdateId.bind(this);

        this.addItem = this.addItem.bind(this);
        this.updateItem = this.updateItem.bind(this);
        this.deleteItem = this.deleteItem.bind(this);
    }
    changeName(event) {
        this.setState({ name: event.target.value });
    }
    changeDescription(event) {
        this.setState({ description: event.target.value });
    }
    changeCategory(event) {
        this.setState({ categoryName: event.target.value })
    }
    changeArticleNumber(event) {
        this.setState({ articleNumber: event.target.value })
    }
    changePrice(event) {
        this.setState({ price: event.target.value })
    }
    changeSelectedUpdateId = (event) => {               
        let product = this.props.products.find(x => x.props.id == event.target.value);
        this.setState({
            selectedUpdateId: event.target.value,
            updateName: product.props.name,
            updateDescription: product.props.description,
            updateCategoryName: product.props.categoryName,
            updateArticleNumber: product.props.articleNumber,
            updatePrice: product.props.price,
        })
    }
    changeUpdateName = (event) => this.setState({ updateName: event.target.value })
    changeUpdateDescription = (event) => this.setState({ updateDescription: event.target.value })
    changeUpdatePrice = (event) => this.setState({ updatePrice: event.target.value })
    changeUpdateArticleNumber = (event) => this.setState({ updateArticleNumber: event.target.value })
    changeUpdateCategoryName = (event) => this.setState({ updateCategoryName: event.target.value })
    changeSelectedDeleteId = (event) => this.setState({ selectedDeleteId: event.target.value})

    addItem = (event) => {
        let data = JSON.stringify({
            name: this.state.name,
            description: this.state.description,
            price: this.state.price,
            categoryName: this.state.categoryName,
            articleNumber: this.state.articleNumber
        });
        let fetchurl = this.props.myApiUrl + "/Products";
        fetch(fetchurl, {
            method: 'post',
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('jwtbearer')}`
            },
            body: data

        }).then(res => res.json())
            .then(l => {
                console.log(l);
                alert(`Produkt ${l.name} skapades.`);
            });
        event.preventDefault();
    }
    updateItem = (event) => {
        if (this.state.selectedUpdateId === 0) {
            alert("Du måste välja ett produktId")
            event.preventDefault();

            return;
        }
        let data = JSON.stringify({
            name: this.state.updateName,
            description: this.state.updateDescription,
            price: this.state.updatePrice,
            categoryName: this.state.updateCategoryName,
            articleNumber: this.state.updateArticleNumber
        });
        let fetchurl = this.props.myApiUrl + "/Products/" + `${this.state.selectedUpdateId}`
        console.log(fetchurl);
        console.log(data);
        fetch(fetchurl, {
            method: 'put',
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('jwtbearer')}`
            },
            body: data

        }).then(res => res.json())
            .then(l => {
                console.log(l);
                alert(`Produkt ${l.name} uppdaterades.`)
            });
        event.preventDefault();
    }
    deleteItem(event) {
        if (this.state.selectedDeleteId === 0) {
            alert("Du måste välja ett produktId")
            event.preventDefault();

            return;
        }
        let fetchurl = this.props.myApiUrl + "/Products/" + `${this.state.selectedDeleteId}`;
        fetch(fetchurl, {
            method: 'delete',
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('jwtbearer')}`
            }
        }).then(res => {
            if (res.status === 200) {
                alert("Produkt borttagen");
            }
            else alert("Något gick fel");
        });
        event.preventDefault();

    }
    render() {
        return (
            <div>
                <form autoComplete="off" className="productForm" onSubmit={this.addItem}>
                    <h3>Lägg till produkt</h3>
                    <div className="mb-3">
                        <label className="form-label">Produktnamn</label>
                        <input required className="form-control col-4" type="text" value={this.state.name} onChange={this.changeName} />
                        <label className="form-label">Beskrivning</label>
                        <input required className="form-control col-4" type="text" value={this.state.description} onChange={this.changeDescription} />
                        <label className="form-label">Kategori</label>
                        <input required className="form-control col-4" type="text" value={this.state.categoryName} onChange={this.changeCategory} />
                        <label className="form-label">Artikelnummer</label>
                        <input required className="form-control col-4" type="text" value={this.state.articlenumber} onChange={this.changeArticleNumber} />
                        <label className="form-label">Pris</label>
                        <input required className="form-control col-4" type="number" value={this.state.price} onChange={this.changePrice} />

                    </div>
                    <input type="submit" className="btn btn-primary" value="Lägg till" />
                </form>
                <br />
                <br />
                <form autoComplete="off" className="productForm" onSubmit={this.updateItem}>
                    <h3>Uppdatera produkt</h3>
                    <div className="mb-3">
                        <label className="form-label">ProduktId</label>
                        <div className="col-1">
                            <select required className="form-control col-4" type="text" id="productName" value={this.state.selectedUpdateId} onChange={this.changeSelectedUpdateId}>                                
                                {this.props.products.map(x => <option key={x.props.id}>{x.props.id}</option>)}
                            </select>
                        </div>
                        <label className="form-label">Produktnamn</label>
                        <input required className="form-control col-4" type="text" value={this.state.updateName} onChange={this.changeUpdateName} />
                        <label className="form-label">Beskrivning</label>
                        <input required className="form-control col-4" type="text" value={this.state.updateDescription} onChange={this.changeUpdateDescription} />
                        <label className="form-label">Kategori</label>
                        <input required className="form-control col-4" type="text" value={this.state.updateCategoryName} onChange={this.changeUpdateCategoryName} />
                        <label className="form-label">Artikelnummer</label>
                        <input required className="form-control col-4" type="text" value={this.state.updateArticleNumber} onChange={this.changeUpdateArticleNumber} />
                        <label className="form-label">Pris</label>
                        <input required className="form-control col-4" type="number" value={this.state.updatePrice} onChange={this.changeUpdatePrice} />

                    </div>
                    <input type="submit" className="btn btn-primary" value="Uppdatera" />
                </form>
                <br />
                <br />
                <form autoComplete="off" className="productForm" onSubmit={this.deleteItem}>
                    <h3>Ta bort produkt</h3>
                    <div className="col-1">
                        <select defaultValue="id" required className="form-control col-4" type="text" value={this.state.selectedDeleteId} onChange={this.changeSelectedDeleteId}>    
                            {this.props.products.map(x => <option key={x.props.id}>{x.props.id}</option>)}
                        </select>
                    </div>
                    <div className="col-2"><input type="submit" className="btn btn-danger" value="Delete" /></div>
                </form>
            </div>
        );
    }
}