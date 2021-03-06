﻿using System;
using System.IO;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Tests
{
    public class SerializationTests
    {
        private readonly Checklist checklist_;
        private readonly ITestOutputHelper testOutputHelper_;

        public SerializationTests(ITestOutputHelper testOutputHelper)
        {
            testOutputHelper_ = testOutputHelper;
            Console.SetOut(new TestOutputWriter(testOutputHelper_));
            checklist_ = ChecklistTestHelper.BuildChecklist();
        }

        [Fact]
        public void Can_serialize_checklist()
        {
            string json = ChecklistFactory.Serialize(checklist_);
            json.Should().NotBeEmpty();
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "checklist.json"), json);
        }

        [Fact]
        public void Can_parse_checklist()
        {
            var json = File.ReadAllText("./Data/checklist.json");
            var checklist = ChecklistFactory.Parse(json);
            ChecklistTestHelper.ChecklistTreeStructureShouldBeConsistent(checklist);
        }

        [Fact]
        public void Can_serialize_then_parse_checklist()
        {
            ChecklistTestHelper.ChecklistTreeStructureShouldBeConsistent(checklist_);
            string json = ChecklistFactory.Serialize(checklist_);
            var checklist = ChecklistFactory.Parse(json);
            ChecklistTestHelper.ChecklistTreeStructureShouldBeConsistent(checklist);
        }
    }
}
