using System.Text.Json.Serialization;
using OpenAI.ObjectModels.SharedModels;

namespace OpenAI.ObjectModels.ResponseModels;

public record RunStepListResponse : DataWithPagingBaseResponse<List<RunStepResponse>>
{
}

public record RunStepResponse : BaseResponse, IOpenAiModels.IId, IOpenAiModels.ICreatedAt
{
    /// <summary>
    ///     The ID of the [assistant](/docs/api-reference/assistants) associated with the run step.
    /// </summary>
    [JsonPropertyName("assistant_id")]
    public string AssistantId { get; set; }

    /// <summary>
    ///     The ID of the [thread](/docs/api-reference/threads) that was run.
    /// </summary>
    [JsonPropertyName("thread_id")]
    public string ThreadId { get; set; }

    /// <summary>
    ///     The ID of the [run](/docs/api-reference/runs) that this run step is a part of.
    /// </summary>
    [JsonPropertyName("run_id")]
    public string RunId { get; set; }

    /// <summary>
    ///     The type of run step, which can be either `message_creation` or `tool_calls`.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    ///     The status of the run step, which can be either `in_progress`, `cancelled`, `failed`, `completed`, `expired`, or
    ///     'incomplete'.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("step_details")]
    public RunStepDetails? StepDetails { get; set; }

    /// <summary>
    ///     The last error associated with this run step. Will be `null` if there are no errors.
    /// </summary>
    [JsonPropertyName("last_error")]
    public Error? LastError { get; set; }

    /// <summary>
    ///     The Unix timestamp (in seconds) for when the run step expired. A step is considered expired if the parent run is
    ///     expired.
    /// </summary>
    [JsonPropertyName("expired_at")]
    public int? ExpiredAt { get; set; }

    /// <summary>
    ///     The Unix timestamp (in seconds) for when the run step was cancelled.
    /// </summary>
    [JsonPropertyName("cancelled_at")]
    public int? CancelledAt { get; set; }

    /// <summary>
    ///     The Unix timestamp (in seconds) for when the run step failed.
    /// </summary>
    [JsonPropertyName("failed_at")]
    public int? FailedAt { get; set; }

    /// <summary>
    ///     The Unix timestamp (in seconds) for when the run step completed.
    /// </summary>
    [JsonPropertyName("completed_at")]
    public int? CompletedAt { get; set; }

    /// <summary>
    ///     Set of 16 key-value pairs that can be attached to an object. This can be useful for storing additional information
    ///     about the object in a structured format. Keys can be a maximum of 64 characters long and values can be a maxium of
    ///     512 characters long.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>
    ///     Usage statistics related to the run step. This value will be `null` while the run step&apos;s status is
    ///     `in_progress`.
    /// </summary>
    [JsonPropertyName("usage")]
    public UsageResponse? Usage { get; set; }

    /// <summary>
    ///     The Unix timestamp (in seconds) for when the run step was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public int CreatedAt { get; set; }

    /// <summary>
    ///     The identifier of the run step, which can be referenced in API endpoints.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }
}

public record RunStepDetails
{
    /// <summary>
    ///     Always message_creation.
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("message_creation")]
    public RunStepMessageCreation MessageCreation { get; set; }

    public class RunStepMessageCreation
    {
        [JsonPropertyName("message_id")]
        public string MessageId { get; set; }
    }

    /// <summary>
    /// An array of tool calls the run step was involved in. These can be associated with
    /// one of three types of tools: code_interpreter, file_search, or function.
    /// </summary>
    [JsonPropertyName("tool_calls")]
    public List<Tool>? toolCalls { get; set; }

    public class Tool
    {
        /// <summary>
        /// The ID of the tool call.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The type of tool call.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Details of the Code Interpreter tool call the run step was involved in.
        /// </summary>
        [JsonPropertyName("code_interpreter")]
        public InputOutput? CodeInterpreter { get; set; }

        /// <summary>
        /// For now, this is always going to be an empty object.
        /// </summary>
        [JsonPropertyName("file_search")]
        public object? FileSearch { get; set; }

        /// <summary>
        /// The definition of the function that was called.
        /// </summary>
        [JsonPropertyName("function")]
        public FunctionObject? Function { get; set; }

        public class InputOutput
        {
            /// <summary>
            /// The input to the Code Interpreter tool call.
            /// </summary>
            [JsonPropertyName("input")]
            public string? Input { get; set; }

            /// <summary>
            /// The outputs from the Code Interpreter tool call. Code Interpreter can output one or more items,
            /// including text (logs) or images (image).
            /// Each of these are represented by a different object type.
            /// </summary>
            [JsonPropertyName("outputs")]
            public List<OutputsObject>? Outputs { get; set; }

            public class OutputsObject
            {
                [JsonPropertyName("type")]
                public string? Type { get; set; }

                /// <summary>
                /// The text output from the Code Interpreter tool call.
                /// </summary>
                [JsonPropertyName("logs")]
                public string? Logs { get; set; }

                [JsonPropertyName("image")]
                public ImageObject? Image { get; set; }

                public class ImageObject
                {
                    /// <summary>
                    /// The file ID of the image.
                    /// </summary>
                    [JsonPropertyName("file_id")]
                    public string? FileId { get; set; }
                }
            }
        }

        public class FunctionObject
        {
            /// <summary>
            /// The name of the function.
            /// </summary>
            [JsonPropertyName("name")]
            public string? Name { get; set; }

            /// <summary>
            /// The arguments passed to the function.
            /// </summary>
            [JsonPropertyName("arguments")]
            public string? Arguments { get; set; }

            /// <summary>
            /// The output of the function. This will be null if the outputs have not been submitted yet.
            /// </summary>
            [JsonPropertyName("output")]
            public string? Output { get; set; }
        }
    }
}