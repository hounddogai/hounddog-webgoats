package com.ii.app.exceptions;

import com.ii.app.dto.out.UserOut;
import com.ii.app.repositories.UserRepository;
import com.ii.app.services.UserServiceImpl;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.MessageSource;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.AccessDeniedException;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.servlet.config.annotation.EnableWebMvc;

import javax.naming.AuthenticationException;
import javax.validation.ConstraintViolationException;
import java.util.Arrays;
import java.util.List;
import java.util.Locale;
import java.util.stream.Collectors;
import io.sentry.Sentry;

@ControllerAdvice
public class GlobalExceptionHandler {
    private static Logger logger = LoggerFactory.getLogger(UserServiceImpl.class);

   static  {
       Sentry.init(options -> {
           options.setDsn("https://22af5eae54cb4d80a6156c7a4d579806@o4505369361317888.ingest.sentry.io/4505369363808256");
           // Set tracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
           // We recommend adjusting this value in production.
           options.setTracesSampleRate(1.0);
           // When first trying Sentry it's good to see what the SDK is doing:
           options.setDebug(true);
       });

    }

    private final MessageSource messageSource;
    private static final String UNEXPECTED_ERROR = "Exception.unexpected";
    private static final String AUTHENTICATION_ERROR = "Exception.authentication";

    private UserServiceImpl userService;

    @Autowired
    public void setUserService(UserServiceImpl userService) {
        this.userService = userService;
    }

    @Autowired
    public GlobalExceptionHandler(MessageSource messageSource) {
        this.messageSource = messageSource;
    }

    @ExceptionHandler(ApiException.class)
    public ResponseEntity<ApiResponse> handleIllegalArgs(ApiException exception, Locale locale) {
        String errMessage = this.messageSource.getMessage(exception.getMessage(), exception.getArgs(), locale);
        UserOut currentUserOrNull = userService.findCurrentUserOrNull();
        logger.error("Error for user " + currentUserOrNull, new Throwable());
        RuntimeException runtimeException = new RuntimeException("Error for user " + currentUserOrNull);
		try{
        Sentry.captureException(runtimeException);
        return new ResponseEntity<>(new ApiResponse(errMessage), HttpStatus.BAD_REQUEST);
		}
		catch(Exception ex){
			logger.info(String.valueOf(runtimeException));
            throw ex;
		}
    }

    @ExceptionHandler(MethodArgumentNotValidException.class)
    public ResponseEntity<ApiResponse> handleInvalidArgs(MethodArgumentNotValidException exception, Locale locale) {
        BindingResult bindingResult = exception.getBindingResult();
        List<String> errMessages = bindingResult.getAllErrors()
            .stream()
            .map(e -> messageSource.getMessage(e, locale))
            .collect(Collectors.toList());
        UserOut currentUserOrNull = userService.findCurrentUserOrNull();
        logger.error("Error for user " + currentUserOrNull, new Throwable());
        RuntimeException runtimeException = new RuntimeException("Error for user " + currentUserOrNull);
        Sentry.captureException(runtimeException);
        return new ResponseEntity<>(new ApiResponse(errMessages), HttpStatus.BAD_REQUEST);
    }

    @ExceptionHandler(Exception.class)
    public ResponseEntity<ApiResponse> handleExceptions(Exception exception, Locale locale) {
        String errMessage;
        if (exception instanceof AccessDeniedException) {
            errMessage = messageSource.getMessage(AUTHENTICATION_ERROR, null, locale);
            return new ResponseEntity<>(new ApiResponse(errMessage), HttpStatus.FORBIDDEN);
        }        errMessage = messageSource.getMessage(UNEXPECTED_ERROR, null, locale);

        System.out.println(exception.getMessage());
        UserOut currentUserOrNull = userService.findCurrentUserOrNull();
        logger.error("Error for user " + currentUserOrNull, new Throwable());
        RuntimeException runtimeException = new RuntimeException("Error for user " + currentUserOrNull);
        Sentry.captureException(runtimeException);
        return new ResponseEntity<>(new ApiResponse(errMessage), HttpStatus.INTERNAL_SERVER_ERROR);
    }
}