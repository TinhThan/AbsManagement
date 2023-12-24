import { Modal, notification } from "antd";
import { ReactNode } from "react";

let countMessage = 0;

class MessageBox {
    static Success(message) {
        Modal.success({
            centered: true,
            title: "Thông báo",
            focusTriggerAfterClose: false,
            getContainer() {
                return document.getElementById('root') || document.body;
            },
            onCancel() {
                countMessage = 0;
            },
            content: message,
            zIndex: 1001,
        });
    }

    static Info(message) {
        Modal.info({
            focusTriggerAfterClose: false,
            centered: true,
            title: "Thông báo",
            content: message,
        });
    }

    static Warning(message, param) {
        if (countMessage > 0) {
            return;
        }
        countMessage++;
        Modal.warning({
            focusTriggerAfterClose: false,
            centered: true,
            onCancel() {
                countMessage = 0;
            },
            className: 'modal-component',
            getContainer() {
                return document.getElementById('root') || document.body;
            },
            title: "Thông báo",
            content: message,
            onOk: () => {
                countMessage = 0;
                return param?.onOk && param.onOk();
            },
            autoFocusButton: 'cancel',
            zIndex: 1001,
        });
    }

    static Fail(message, param) {
        if (countMessage > 0) {
            return;
        }
        countMessage++;
        Modal.warning({
            centered: true,
            focusTriggerAfterClose: false,
            okText: "Ok",
            cancelText: "Hủy",
            title: "Thông báo",
            onCancel() {
                countMessage = 0;
            },
            getContainer() {
                return document.getElementById('root') || document.body;
            },
            content: message,
            onOk: () => {
                countMessage = 0;
                return param?.onOk && param.onOk();
            },
            zIndex: 1001,
        });
    }

    static Confirm(param) {
        if (countMessage > 0) {
            return;
        }
        countMessage++;
        const modal = Modal.confirm({
            ...param,
            centered: true,
            title: "Thông báo",
            focusTriggerAfterClose: false,
            getContainer() {
                return document.getElementById('root') || document.body;
            },
            content: param.content,
            onOk: () => {
                countMessage = 0;
                modal.destroy();
                if (param.onOk) {
                    return param.onOk();
                }
            },
            autoFocusButton: 'cancel',
            onCancel: (arg) => {
                countMessage = 0;
                if (param.onCancel) {
                    param.onCancel(arg);
                }
                countMessage = 0;
                modal.destroy();
            },
            cancelText: "Hủy",
            zIndex: 1001,
        });
    }

    static Warning_Html(message, param) {
        if (countMessage > 0) {
            return;
        }
        countMessage++;
        Modal.warning({
            centered: true,
            focusTriggerAfterClose: false,
            getContainer() {
                return document.getElementById('root') || document.body;
            },
            title: "Thông báo",
            content: message,
            onCancel() {
                countMessage = 0;
                if (param?.onCancel) {
                    param.onCancel();
                }
            },
            onOk: () => {
                countMessage = 0;
                return param?.onOk && param.onOk();
            },
            zIndex: 1001,
        });
    }
}

class Notification {
    static Success(description) {
        notification.success({
            message: "Thông báo",
            description: description,
            duration: 2
        });
    }

    static Warning(description) {
        notification.warning({
            message: "Thông báo",
            description: description
        });
    }

    static Fail(description) {
        notification.error({
            message: "Thông báo",
            description: description
        });
    }

    static Info_Html(description) {
        notification.info({
            message: "Thông báo",
            description: description,
        });
    }
}

export { MessageBox, Notification };
