using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Stylization
{
    public class ProcessResult
    {
        public ProcessResult(ResultStatus status)
        {
            this.status = status;
            message = "";
        }

        public ProcessResult(ResultStatus status, string message)
        {
            this.status = status;
            this.message = message;
        }

        public ProcessResult(ResultStatus status, Object associatedObject)
        {
            this.status = status;
            this.associatedObject = associatedObject;
        }

        public ProcessResult(ResultStatus status, string message, Object associatedObject)
        {
            this.status = status;
            this.message = message;
            this.associatedObject = associatedObject;
        }

        public enum ResultStatus
        {
            Success,
            Error
        }

        private ResultStatus status;

        public ResultStatus Status { get { return status; } }

        private string message = "";

        public string Message { get { return message; } }

        private Object associatedObject;

        public Object AssociatedObject { get { return associatedObject; } }
    }

}