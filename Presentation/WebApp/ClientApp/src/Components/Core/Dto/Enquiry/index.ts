import { AuditableDto } from "../AuditableDto";
import { IUserDto } from "../Users";

export interface IEnquiryDto extends AuditableDto {
    email: string;
    name: string;
    message: string;
    resolution: string;
    isResolved: boolean;
    subject: string;
    updatedBy: IUserDto;
}