import * as $ from "jquery";
import * as React from "react";
import { Portal } from "../components/Portal";
import { PortalHeader } from "../components/PortalElements";
import { NotificationCenterContainer } from "./NotificationCenterContainer";
const fixLayout = () => {
    // Get window height and the wrapper height
    const neg = $(".main-header").outerHeight() + $(".main-footer").outerHeight();
    const windowHeight = $(window).height();
    const sidebarHeight = $(".sidebar").height();
    // Set the min-height of the content and sidebar based on the
    // the height of the document.
    if ($("body").hasClass("fixed")) {
        $(".content-wrapper, .right-side").css("min-height", windowHeight - $(".main-footer").outerHeight());
    } else {
        let postSetWidth;
        if (windowHeight >= sidebarHeight) {
            $(".content-wrapper, .right-side").css("min-height", windowHeight - neg);
            postSetWidth = windowHeight - neg;
        } else {
            $(".content-wrapper, .right-side").css("min-height", sidebarHeight);
            postSetWidth = sidebarHeight;
        }
        // Fix for the control sidebar height
        const controlSidebar = $(".control-sidebar");
        if (typeof controlSidebar !== "undefined") {
            if (controlSidebar.height() > postSetWidth) {
                $(".content-wrapper, .right-side").css("min-height", controlSidebar.height());
            }
        }
    }
};

export class PortalContainer extends React.Component<any, any> {
// redefined refs type to any to bypass type check.
    public refs: {
        [string: string]: any;
        stepInput: any;
    };
    private push: () => void;
    constructor(props) {
        super(props);
        this.state = { token: "", notifications: [] as Notification[], filter: "" };
        this.push = () => { };
        this.handleFilterChange = this.handleFilterChange.bind(this);
    }

    public handleFilterChange(e) {
        this.setState({
            [e.target.name]: e.target.value as string,
        });
    }
    public componentWillMount() {
        this.state.token = this.props.location.query.access_token;
        if (this.state.token) {
            window.history.pushState(null, null, "?authorized");
        }
    }
    public componentDidUpdate() {
        fixLayout();
    }
    public componentDidMount() {
        this.push = this.refs.nc.pushNotification;
    }

    public render() {
        return (
            <Portal token={this.state.token}>
                <PortalHeader
                    token={this.state.token}
                    filter={this.state.filter}
                    handleFilterChange={this.handleFilterChange}
                />
                <NotificationCenterContainer ref="nc" />
                {React.cloneElement(this.props.children,
                    { token: this.state.token, push: this.push, filter: this.state.filter })}
            </Portal>
        );
    }
}
