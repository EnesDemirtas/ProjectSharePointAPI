﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSP.Application.Enums;

namespace PSP.Application.Models {

    public class OperationResult<T> {
        public T Payload { get; set; }
        public bool IsError { get; private set; }
        public List<Error> Errors { get; } = new List<Error>();

        public void AddError(ErrorCode code, string message) {
            HandleError(code, message);
        }

        public void AddUnknownError(string message) {
            HandleError(ErrorCode.UnknownError, message);
        }

        public void ResetIsErrorFlag() {
            IsError = false;
        }

        #region Private methods

        private void HandleError(ErrorCode code, string message) {
            Errors.Add(new Error { Code = code, Message = message });
            IsError = true;
        }

        #endregion Private methods
    }
}