﻿namespace TestExercise.DTOs.Group;

public record GetGroupDTO(int Id, string Name, List<int> Students);