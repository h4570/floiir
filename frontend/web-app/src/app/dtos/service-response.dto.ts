export interface ServiceResponse<DataType, ResponseCodeType> {

    /* data provided by http request */
    data: DataType;

    /** response code enum  */
    responseCode: ResponseCodeType;

}

