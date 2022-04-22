import * as React from "react";
import * as ReactDOM from "react-dom";
import { Navbar } from "./navbar";
import { Products} from "./Products/products"
import { Orders } from "./Orders/orders"
import { Users } from "./Users/users"
import { Login } from "./Login/login"


class App extends React.Component {
    constructor(props) {
        super(props);
        this.state = {     
            showLogin : true,
            showProducts : false,
            showOrders: false,
            showUsers:false,
            isAdmin: false,
            loggedIn : false,
            myApiUrl: ''
        }
        this.showSelectedPage = this.showSelectedPage.bind(this);
        this.setAdmin = this.setAdmin.bind(this);
        this.setLoggedIn = this.setLoggedIn.bind(this);
    }
    componentDidMount() { 
        let json = require('./apiurl.json')
        let readstring = json.apiurl;
        console.log(readstring);
        this.setState({myApiUrl: readstring});
    }
    showSelectedPage = (component) => {
        
        switch (component){
            case 1: this.setState({
                showLogin : true,
                showProducts : false,
                showOrders: false,
                showUsers: false
            })
            break;
            case 2: this.setState({
                showLogin : false,
                showProducts : true,
                showOrders: false,
                showUsers: false
            })
            break;
            case 3: this.setState({
                showLogin : false,
                showProducts : false,
                showOrders: true,
                showUsers: false
            })
            break;
            case 4: this.setState({
                showLogin : false,
                showProducts : false,
                showOrders: false,
                showUsers: true
            })
            break;
            case 5: this.setState({
                showLogin : true,
                showProducts : false,
                showOrders: false,
                showUsers: false
            })
        
            localStorage.removeItem('jwtbearer');
            localStorage.removeItem('userId');
            this.setState({
                isAdmin : false,
                loggedIn : false
            })
            break;
            
            default:this.setState({
                showLogin : true,
                showProducts : false,
                showOrders: false,
                showUsers: false
            })
            break;
        }

    }
    setAdmin(value){
        this.setState({
            isAdmin: value
        })
    }
    setLoggedIn(){
        this.setState({
            loggedIn: true
        })
    }

    render() {
        const {showLogin, showProducts, showOrders, showUsers} = this.state;
        return (
            <div>
                <Navbar isLoggedIn={this.state.loggedIn} showPage={this.showSelectedPage}/> 
                {showLogin && <Login isLoggedIn={this.state.loggedIn} myApiUrl={this.state.myApiUrl} onLogIn={this.setLoggedIn} onAdminSet={this.setAdmin} onGoToPage={this.showSelectedPage}/>}
                {showProducts  && <Products myApiUrl={this.state.myApiUrl} isAdmin={this.state.isAdmin}/>}
                {showOrders && <Orders myApiUrl={this.state.myApiUrl} isAdmin={this.state.isAdmin}/>}
                {showUsers && <Users myApiUrl={this.state.myApiUrl} showPage={this.showSelectedPage} loggedIn={this.state.loggedIn} isAdmin={this.state.isAdmin}/>}               
            </div>

            
        );
    }
}

ReactDOM.render(<App />, document.getElementById("root"));