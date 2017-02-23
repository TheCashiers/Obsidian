import * as $ from "jquery";

export enum NotificationState{
    info,
    error,
    caution,
    success
}

export class Service{
    private static push(content:string,state?:NotificationState){
        let ncStyle:string = "";
        switch(state){
            case NotificationState.caution:
                ncStyle="alert-warning";
                break;
            case NotificationState.error:
                ncStyle="alert-danger";
                break;
            case NotificationState.info:
                ncStyle="alert-info";
                break;
            case NotificationState.success:
                ncStyle="alert-success";
                break;
            default:
                ncStyle="alert-info";
        }
        var options =  {
            content: content, // text of the snackbar
            style: `alert ${ncStyle}`, // add a custom class to your snackbar
            timeout: 3000, // time in milliseconds after the snackbar autohides, 0 is disabled
            htmlAllowed: true, // allows HTML as content value
        };
        $.snackbar(options);
    }
    static pushError(desc:string,error:Error){
        this.push(`${desc} failed. ${error.toString()}.`,NotificationState.error)
    }
    static pushSuccess(desc:string){
        this.push(`${desc} success.`,NotificationState.success)
    }
}