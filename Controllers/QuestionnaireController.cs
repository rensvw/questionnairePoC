using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using questionnaireBackend;
using questionnaireBackend.wrapper;

namespace questionnaireBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private readonly QuestionnaireContext _context;

        public QuestionnaireController(QuestionnaireContext context)
        {
            _context = context;
        }

        // GET: api/Questionnaire
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionnaireViewModel>>> GetQuestionnaires()
        {
            var questionnaires = await _context.Questionnaire.ToListAsync();
            if (questionnaires == null) throw new ArgumentNullException(nameof(questionnaires));

            return questionnaires.Select(questionnaire => 
                new QuestionnaireWrapper(questionnaire).GetViewModel())
                .ToList();
        }

        // GET: api/Questionnaire/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionnaireViewModel>> GetQuestionnaire(string id)
        {
            var questionnaire = await _context.Questionnaire.FindAsync(id);


            if (questionnaire == null)
            {
                return NotFound();
            }
            
            return new QuestionnaireWrapper(questionnaire).GetViewModel() ?? 
                   throw new ArgumentNullException("id");
        }

        // PUT: api/Questionnaire/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionnaire(string id, Questionnaire questionnaire)
        {
            if (id != questionnaire.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionnaire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionnaireExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Questionnaire
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Questionnaire>> PostQuestionnaire(QuestionnaireViewModel questionnaire)
        {
            var wrappedQuestionnaire = new QuestionnaireWrapper(questionnaire).GetStoreModel() 
                                       ?? throw new ArgumentNullException("questionnaire");

            _context.Questionnaire.Add(wrappedQuestionnaire);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuestionnaireExists(wrappedQuestionnaire.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetQuestionnaire", new { id = wrappedQuestionnaire.Id }, wrappedQuestionnaire);
        }

        // DELETE: api/Questionnaire/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Questionnaire>> DeleteQuestionnaire(string id)
        {
            var questionnaire = await _context.Questionnaire.FindAsync(id);
            if (questionnaire == null)
            {
                return NotFound();
            }

            _context.Questionnaire.Remove(questionnaire);
            await _context.SaveChangesAsync();

            return questionnaire;
        }

        private bool QuestionnaireExists(string id)
        {
            return _context.Questionnaire.Any(e => e.Id == id);
        }
    }
}
