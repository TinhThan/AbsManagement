import { Modal, ModalFuncProps, notification } from "antd";
import { Language } from "./languages";
import { ReactNode } from "react";

let countMessage = 0;
export class MessageBox {
    public static Success(message: string): void {
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

    public static Info(message: string): void {
      Modal.info({
        focusTriggerAfterClose: false,
        centered: true,
        title: "Thông báo",
        content: message,
      });
    }
    
    public static Warning(message: ReactNode, param?: ModalFuncProps): void {
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
  
    public static Fail(message: ReactNode, param?: ModalFuncProps): void {
      if (countMessage > 0) {
        return;
      }
      countMessage++;
      Modal.warning({
        centered: true,
        focusTriggerAfterClose: false,
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
  
    public static Confirm(param: ModalFuncProps): void {
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
  
    public static Warning_Html(message: ReactNode, param?: ModalFuncProps): void {
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

export class Notification{
    public static Success(description:string):void{
        notification.success({
            message: Language.Notification,
            description: description,
            duration:2
        })
    }

    public static Warning(description: string): void{
        notification.warning({
            message: Language.Notification,
            description:description
        })
    }

    public static Fail(description: ReactNode): void{
        notification.error({
            message: Language.Notification,
            description: description
        })
    }

    public static Info_Html(description: ReactNode): void {
        notification.info({
            message: Language.Notification,
            description: description,
        });
    }
}