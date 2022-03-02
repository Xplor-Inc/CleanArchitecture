﻿using ExpressCargo.Core.Interfaces.Emails.EmailHandler;
using ExpressCargo.Core.Interfaces.Emails.Templates;
using ExpressCargo.Core.Models.Entities.Enquiries;
using ExpressCargo.WebApp.Models.Dtos.Enquiries;

namespace ExpressCargo.WebApp.EndPoints.Enquiries;

[Route("api/1.0/enquiries")]
public class EnquiryController : ExpressCargoController
{
    private IRepositoryConductor<Enquiry>    ContactConductor   { get; }
    private IEmailHandler                    EmailHandler       { get; }
    private IHtmlTemplate                    HtmlTemplate       { get; }
    private IMapper                          Mapper             { get; }

    public EnquiryController(
        IRepositoryConductor<Enquiry>   contactConductor,
        IEmailHandler                   emailHandler,
        IHtmlTemplate                   htmlTemplate,
        IMapper                         mapper)
    {
        ContactConductor    = contactConductor;
        EmailHandler        = emailHandler;
        HtmlTemplate        = htmlTemplate;
        Mapper              = mapper;
    }


    [AppAuthorize]
    [HttpGet]
    public IActionResult Index(
        string      searchText,
        string      sortBy      = "CreatedOn",
        string      sortOrder   = "DESC",
        int         skip        = 0,
        int         take        = 5)
    {
        Expression<Func<Enquiry, bool>> predicate = e => true;

        if (!string.IsNullOrEmpty(searchText))
        {
            predicate = predicate.AndAlso(e => e.Name.Contains(searchText) 
                                            || e.Email.Contains(searchText)
                                            || e.Message.Contains(searchText)
                                            || e.Subject.Contains(searchText));
        }

        var enquiryResult = ContactConductor.FindAll(filter: predicate, e => e.OrderBy(sortBy, sortOrder), skip: skip, take: take);
        if (enquiryResult.HasErrors)
        {
            return InternalError<EnquiryDto>(enquiryResult.Errors);
        }
        var enquiries = enquiryResult.ResultObject.ToList();
        var rowCount = ContactConductor.FindAll(filter: predicate).ResultObject.Select(e => e.Id).Count();
        var dtos = Mapper.Map<List<EnquiryDto>>(enquiries);
        return Ok(dtos, rowCount);
    }

    [HttpPost]
    public IActionResult Post([FromBody] EnquiryDto dto)
    {       
        var enquiry = Mapper.Map<Enquiry>(dto);

        var createResult = ContactConductor.Create(enquiry, CurrentUserId);
        if (createResult.HasErrors)
        {
            return InternalError<EnquiryDto>(createResult.Errors);
        }

        string emailbody = HtmlTemplate.EnquiryThanks(enquiry);

        EmailHandler.Send(emailbody, dto.Subject, new string[] { dto.Email }); 

        return Ok(createResult.ResultObject);
    }

    [AppAuthorize]
    [HttpPut("{id:Guid}")]
    public IActionResult Put(Guid id, [FromBody] EnquiryResolutionDto dto)
    {
        var enquiryResult = ContactConductor.FindById(id);
        if (enquiryResult.HasErrors)
        {
            return InternalError<EnquiryDto>(enquiryResult.Errors);
        }
        var enquiry = enquiryResult.ResultObject;
        if (enquiry == null)
        {
            return InternalError<EnquiryDto>("Invalid enquiry");
        }

        enquiry.IsResolved   = true;
        enquiry.Resolution   = dto.Resolution;

        var updateResult = ContactConductor.Update(enquiry, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<EnquiryDto>(updateResult.Errors);
        }
        return Ok(updateResult.ResultObject);
    }

    [AppAuthorize]
    [HttpDelete("{id:Guid}")]
    public IActionResult Delete(Guid id)
    {
        var updateResult = ContactConductor.Delete(id, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<EnquiryDto>(updateResult.Errors);
        }
        return Ok(updateResult.ResultObject);
    }
}