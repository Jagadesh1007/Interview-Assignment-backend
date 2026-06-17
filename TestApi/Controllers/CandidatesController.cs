        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CandidateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid request", errors = ModelState });
            }

            if (request.Resume == null)
            {
                return BadRequest(new { success = false, message = "Resume file is required" });
            }

            await _service.SaveCandidateAsync(request);

            return Ok(new { success = true, message = "Application submitted successfully" });
        }
    }
}        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CandidateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid request", errors = ModelState });
            }

            if (request.Resume == null)
            {
                return BadRequest(new { success = false, message = "Resume file is required" });
            }

            await _service.SaveCandidateAsync(request);

            return Ok(new { success = true, message = "Application submitted successfully" });
        }
    }
}