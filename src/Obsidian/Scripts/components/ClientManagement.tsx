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
                    <Link to={{pathname:"/manage/clients/edit/",query:{ id: props.id }}}>
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
        <Link to="/manage/clients/create">
            <button className="btn btn-lg btn-success">Create Client...</button>
        </Link>
        <div className="list-group">
            {props.clients.map((client, index) => <ClientItem clientname={client.displayName} id={client.id} key={client.id} />)}
        </div>
    </div>
);