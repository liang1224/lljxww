﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Lljxww.Test;

[TestClass]
public class JsonNodeTest
{
    [TestMethod]
    public void JsonNodeInitTest()
    {
        string? jsonStr = JsonSerializer.Serialize(TestModelGen(otherModelCount: 5));

        JsonNode jsonNode = JsonNode.Parse(jsonStr, new JsonNodeOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        _ = jsonNode.AsObject();
        JsonArray jsonArray = jsonNode["othermodels"]!.AsArray();
        _ = jsonNode["name"]!.AsValue();
        _ = JsonDocument.Parse(jsonStr);
        _ = jsonArray.Deserialize<IList<Dictionary<string, JsonNode>>>();
    }

    public TestModel TestModelGen(int bookCount = 5, int otherModelCount = 0)
    {
        Random random = new();

        List<string>? books = [];
        for (int i = 0; i < bookCount; i++)
        {
            books.Add(Guid.NewGuid().ToString().Replace("-", ""));
        }

        List<TestModel>? otherModels = [];
        for (int j = 0; j < otherModelCount; j++)
        {
            otherModels.Add(TestModelGen());
        }

        return new TestModel
        {
            Id = random.Next(10000),
            Name = Guid.NewGuid().ToString().Replace("-", ""),
            Books = books,
            OtherModels = otherModels
        };
    }

    public class TestModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<string> Books { get; set; }

        public IList<TestModel> OtherModels { get; set; }
    }
}