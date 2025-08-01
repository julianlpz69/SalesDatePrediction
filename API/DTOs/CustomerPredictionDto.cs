﻿namespace API.DTOs;

public class CustomerPredictionDto
{   
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public DateTime LastOrderDate { get; set; }
    public DateTime NextPredictedOrder { get; set; }
}