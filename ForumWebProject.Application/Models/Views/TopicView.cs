﻿namespace ForumWebProject.Application.Models.Views;

public class TopicView
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid? CategoryId { get; set; }
}