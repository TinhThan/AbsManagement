import { notification } from "antd";
import { Language } from "./languages";
import { ReactNode } from "react";

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