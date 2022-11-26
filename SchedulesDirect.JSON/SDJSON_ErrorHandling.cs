using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulesDirect {
	public class SDJsonErrorHandling {
        private readonly Stack<SDJsonError> _localErrors;

        protected SDJsonErrorHandling() {
            _localErrors = new Stack<SDJsonError>();
        }

        /// <summary>
        /// Return raw error details (if any)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SDJsonError> GetRawErrors() {
            return _localErrors.AsEnumerable();
        }
		
        /// <summary>
        /// Return true if there are errors to report
        /// </summary>
        public bool HasErrors => _localErrors.Count > 0;

        /// <summary>
        /// Retrieve the most recent reported error. If specified pop (remove) after returning
        /// </summary>
        /// <param name="pop"></param>
        /// <returns></returns>
        public SDJsonError GetLastError(bool pop = true) {
            if (pop) {
                return _localErrors.Pop();
            } else {
                return _localErrors.Peek();
            }
		}
		
		/// <summary>
		/// Clear any errors
        /// </summary>
        public void ClearErrors() {
            _localErrors.Clear();
        }
		
        protected void AddError(Exception ex) {
            _localErrors.Push(new SDJsonError(ex));
        }
		
        protected void addError(int errorcode, string errormessage, SDJsonError.ErrorSeverity errorseverity = SDJsonError.ErrorSeverity.Error, string errordescription = "", string errorsource = "") {
            _localErrors.Push(new SDJsonError(errorcode, errormessage, errorseverity, errordescription, errorsource));
        }

        // Queue exception to local errors
        // @Todo: Create local error class, encompassing local errors and exceptions
		protected void QueueError(Exception ex) {
            AddError(ex);
        }

        /// <summary>
        /// Schedules Direct Error Structure
        /// </summary>
        public class SDJsonError {
            public Exception exception;
            public bool isException;
            public int code;
            public string message;
            public string description;
            public string source;
            public ErrorSeverity severity;

            public enum ErrorSeverity {
                Info,
                Warning,
                Error,
                Fatal
            }

            public SDJsonError(Exception ex) {
                isException = true;
                exception = ex;
                code = ex.HResult;
                message = ex.Message;
                description = ex.StackTrace;
                source = ex.Source;
                severity = ErrorSeverity.Fatal;
            }

            public SDJsonError(int errorcode, string errormessage, ErrorSeverity errorseverity = ErrorSeverity.Error, string errordescription = "", string errorsource = "") {
                isException = false;
                exception = null;
                code = errorcode;
                message = errormessage;
                description = errordescription;
                source = errorsource;
                severity = errorseverity;
            }
        }
    }
}
