import { AppErrorCode } from "../common/app-error-code";

export interface ResponseWrapper {
	// DATA
	data: any;
	// SUCCESS
	success: boolean;
	// UNICORN ERROR CODE
	appErrorCode: AppErrorCode;
	// ERROR
	error: string;
}
