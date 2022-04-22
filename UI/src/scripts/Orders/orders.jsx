import * as React from "react";
import * as ReactDOM from "react-dom";
import { Order } from "./order";

export class Orders extends React.Component {
    constructor(props) {
        super(props);  
        this.state = {
            orders : 'Du har inte gjort några beställningar än!'
        }    
       

    }
    componentDidMount(){
        let fetchurl = this.props.myApiUrl + "/orders/" + localStorage.getItem('userId');
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
                    let ordersList = [];                    
                    let i =1;
                    list.forEach(x => {                        
                        ordersList.push(<Order key={i} {...x} />)
                        i++;
                    });
                    this.setState({
                        orders: ordersList
                    })
                })
            }            
        })
    }
   
    render() {
        return (
           <div>
               <h3>Här ser du alla dina ordrar</h3>
               <ul className="row justify-content-around">{this.state.orders}</ul>
               </div>
        );
    }
}