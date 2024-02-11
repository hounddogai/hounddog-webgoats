package com.ii.app;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.scheduling.annotation.EnableAsync;

@SpringBootApplication
@EnableAsync
public class AppApplication {

    public static void main(String[] args) {
        SpringApplication.run(AppApplication.class, args);
		String query_ins = "INSERT INTO call_details (customer_name, customer_address, start_time, end_time, call_outcome_id) VALUES (2, 2, '2020/1/11 9:50:12', '2020/1/11 9:55:35', 2);";
    }


}
