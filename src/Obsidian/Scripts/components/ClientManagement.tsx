import * as React from "react"
import { Link } from "react-router";
import { styles } from "../styles"

export const ClientItem = (props) => (
    <div>
        <div className="list-group-item">
            <div className="row-action-primary">
                <i className="fa fa-server fa-2x"></i>
            </div>
            <div className="row-content">
                <div className="least-content">
                    <div className="btn-group">
                        <a href="javascript:void(0)" className="btn btn-lg btn-primary btn-raised">Primary raised</a>
                        <a href="bootstrap-elements.html" data-target="#" className="btn btn-lg btn-primary btn-raised dropdown-toggle" data-toggle="dropdown"><span className="caret"></span></a>
                        <ul className="dropdown-menu">
                            <li><a href="javascript:void(0)">Action</a></li>
                            <li><a href="javascript:void(0)">Another action</a></li>
                            <li><a href="javascript:void(0)">Something else here</a></li>
                            <li className="divider"></li>
                            <li><a href="javascript:void(0)">Separated link</a></li>
                        </ul>
                    </div>




                    <Link to="/manage/clients/edit/" query={{clientname:props.clientname,id:props.id}}>
                        <button className="btn btn-lg btn-primary btn-raised">
                            Edit
                        </button>    
                    </Link>
                </div>
                <h4 className="list-group-item-heading">{props.clientname}</h4>
                <p className="list-group-item-text">{props.id}</p>
            </div>
        </div>
        <div className="list-group-separator"></div>
    </div>);

export const ClientList = (props) => (
    <div className="content-wrapper content">

        <div className="list-group">
  {props.clients.map((client, index) => <ClientItem name={client.clientName} id={client.id} key={client.id}/>) }
        </div>



    
        
        <Link to="/manage/clients/create">
            <button className="btn btn-lg btn-success">New Client...</button>
        </Link>
    </div>

);