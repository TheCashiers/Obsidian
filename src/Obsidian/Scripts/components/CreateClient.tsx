import * as React from "react"
export const CreateClient = (props) => 
<div className="well bs-component col-md-6">
    <form className="form-horizontal">
        <fieldset>
            <legend>Create client</legend>
            <div className="form-group">
                <label for="inputDisplayName" className="col-md-2 control-label">Display Name</label>

                <div className="col-md-10">
                    <input type="text" className="form-control" id="inputDisplayName" placeholder="Display Name..."/>
                </div>
            </div>

            <div className="form-group">
                <label for="inputRedirectUri" className="col-md-2 control-label">Redirect Uri</label>

                <div className="col-md-10">
                    <input type="text" className="form-control" id="inputRedirectUri" placeholder="Redirect Uri"/>
                </div>
            </div>
        
            <div className="form-group">
                <div className="col-md-10 col-md-offset-2">
                    <button type="button" className="btn btn-default">Cancel</button>
                    <button type="submit" className="btn btn-primary">Submit</button>
                </div>
            </div>
        </fieldset>
    </form>
</div>