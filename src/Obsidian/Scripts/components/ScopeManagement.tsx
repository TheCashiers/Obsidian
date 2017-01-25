import * as React from "react";
import { Link } from "react-router";

const ScopeItem = (props) => (
    <div>
        <div className="list-group-item">
            <div className="row-action-primary">
                <i className="fa fa-user fa-2x"></i>
            </div>
            <div className="row-content">
                <div className="least-content">
                    <Link to="/manage/scopes/edit/" query={{ scopeName: props.scopeName, id: props.id }}>
                        <button className="btn btn-lg btn-primary btn-raised">
                            Edit
            </button>
                    </Link>
                </div>
                <h4 className="list-group-item-heading">{props.scopeName}</h4>

                <p className="list-group-item-text">{props.id}</p>
            </div>
        </div>
        <div className="list-group-separator"></div>
    </div>
);

export const ScopeList = (props) => (
    <div className="content-wrapper content">
        <Link to="/manage/users/create">
            <button className="btn btn-primary btn-lg">Create Scope</button>
        </Link>
        
        <div className="list-group">
            {props.scopes.map((scope, index) => <ScopeItem scopeName={scope.scopeName} id={scope.id} key={scope.id} />)}
        </div>
    </div>

);