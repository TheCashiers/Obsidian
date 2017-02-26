import * as React from "react";
import { Link } from "react-router";

export const List = (props) => (
    <div className="content-wrapper content">
        <Link to={props.createLink}>
            <button className="btn btn-lg btn-success">{props.action}...</button>
        </Link>
        <div className="list-group">
            {props.items.map((item, index) => 
            <Item name={item.displayName||item.userName||item.scopeName} 
                id={item.id} 
                key={item.id} 
                editLink={props.editLink}
                icon={props.icon} />)}
        </div>
    </div>
);

export const Item = (props) => (
    <div>
        <div className="list-group-item">
            <div className="row-action-primary">
                <i className={`fa ${props.icon} fa-2x`}></i>
            </div>
            <div className="row-content">
                <div className="least-content">
                    <Link to={{pathname:props.editLink ,query:{ id: props.id }}}>
                        <button className="btn btn-lg btn-primary btn-raised">
                            Edit
                        </button>
                    </Link>
                </div>
                <h4 className="list-group-item-heading">{props.name}</h4>
                <p className="list-group-item-text">{props.id}</p>
            </div>
        </div>
        <div className="list-group-separator"></div>
    </div>);