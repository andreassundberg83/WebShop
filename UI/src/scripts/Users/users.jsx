import * as React from "react";
import * as ReactDOM from "react-dom";
import { CreateUserForm } from "./createUserForm";
import { UsersNavbar } from "./users-navbar";
import { AdminUserForms } from "./admin-userforms"
import { UserInfo } from "./userinfo";

export class Users extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            showAdmin: false,
            showCreate: true,
            showUserInfo: false
        }
        this.showSelectedPage = this.showSelectedPage.bind(this);
        this.goToPage = this.goToPage.bind(this);

    }
    componentDidMount(){
        if (this.props.loggedIn)
        this.setState({
            showCreate: false,
            showUserInfo: true
        });         
    }
    showSelectedPage(x) {
        switch (x) {
            case 1:
                this.setState({
                    showAdmin: false,
                    showCreate: true,
                    showUserInfo: false
                })
                break;
            case 2:
                this.setState({
                    showAdmin: false,
                    showCreate: false,
                    showUserInfo: true
                })                
                break;
            case 3:
                this.setState({
                    showAdmin: true,
                    showCreate: false,
                    showUserInfo: false
                })
                break;
            default: break;
        }
    }
    goToPage(x){
        this.props.showPage(x);
    }
    render() {        
        return (
            <div>
                <UsersNavbar showPage={this.showSelectedPage} isAdmin={this.props.isAdmin} loggedIn={this.props.loggedIn} />
                {this.state.showCreate && <CreateUserForm onGoToPage={this.goToPage} myApiUrl={this.props.myApiUrl}/>}
                {this.state.showUserInfo && <UserInfo myApiUrl={this.props.myApiUrl}/>}
                {this.state.showAdmin && <AdminUserForms />}
            </div>
        );
    }
}